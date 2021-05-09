/*
* controllers for assignment
*/
const Assignment=require('../models/assignment');
//const AssignmentScore = require('../models/assignmentScore');
const AssignmentQn = require('../models/assignmentQuestion');
const Student = require('../models/student');
const schemas = require('../models/assignmentScore');
const StudentScore = schemas.StudentScore;
const AssignmentScore = schemas.AssignmentScore;


//function to send back a list of assignments
const assignmentList=(req,res)=>{
    console.log(req.params);
    const tutGrp=req.params.tut_grp;
    Assignment.find({ tutGrp:tutGrp} ).sort({assignmentID:1})
    .then((result)=>{
        console.log(result);
        if(result!=null)
        { 
            let assignmentsList=result.map(assignment=>{ 
            let assignmentInfo={
                assignmentID:assignment.assignmentID,
                assignmentName:assignment.assignmentName,
                timeLimit: assignment.timeLimit,
                deadline:assignment.deadline,
            }
             return assignmentInfo; });
             if("[]"=== JSON.stringify(assignmentsList))
             {
                 res.status(400).send('There are currently no assignments in the database');
             }
             else{
        res.status(200).send(assignmentsList);
             }
        }
        else
        {  
            res.status(400).send('There are currently no assignments set by this teacher');
        }
    })
    .catch((err)=>{
        console.log(err);
        console.log('Hello');
        res.status(400).json(err);
    });

}

const assignmentDetails=(req,res)=>{
    const assignmentID=req.params.a_id;
    //using this assignment id return the assignment details and the assignment score details
    //need to return assignment id, assignment name, time limit, deadline, [{matric No, firstName, lastName, attempt_status, score}]
    Assignment.findOne({assignmentID:assignmentID})
    .then(result=>{
        console.log(result);
        if(result!=null)
        {
            AssignmentScore.find({ assignmentID: { $eq: assignmentID } } ).sort({firstName:1})
            .then((scoresResult)=>{
                //scoreResult has the assignmentID, matricNo, firstName, lastName, attemptStatus and score of each student
                
                tempList = []
                let scores=[]
                avgScore= arr => arr.reduce((a,b) => a + b, 0) / arr.length;
                for (i=0; i < scoresResult[0].studentScoreDict.length; i++) {
                    console.log(scoresResult[0].studentScoreDict[i].attemptStatus);
                    scores.push(scoresResult[0].studentScoreDict[i].scores);
                    let temp = {
                        name:scoresResult[0].studentScoreDict[i].firstName + ' ' + scoresResult[0].studentScoreDict[i].lastName,
                        scores: scoresResult[0].studentScoreDict[i].scores,
                        attemptStatus: scoresResult[0].studentScoreDict[i].attemptStatus ===false? 'Not Attempted':'Attempted',
                    }
                    tempList.push(temp)
                    tempList.sort((b,a) => a.scores - b.scores)
                }
                console.log(scores);
                let assDetails={
                    assnt:result,
                    scoresResults:scoresResult,
                    students:tempList,
                    minScore:Math.min(...scores),
                    maxScore:Math.max(...scores),
                    avgScore:avgScore(scores),
                    scores:scores,
                }
                res.status(200).send(assDetails);//sending back Assignment Scores
            })
            .catch(err =>{

                console.log(err);
                res.status(400).send(err);
            });        
        }
        else
        res.status(404).send("Assignment with such an ID does not exist");
    })
    .catch(err =>{

        console.log(err);
        res.status(400).send(err);
    });


};

const newAssignment=async(req,res)=>{
    const assignmentID = req.params.a_id;
    const { assignmentName, timeLimit, questionIDs, deadline, tutGrp } = req.body[0];
    console.log(req.body[0]);

    //Loop 1 to check if there is any error in looping through
    try {
        const assignment = new Assignment({
            assignmentID:assignmentID,
            assignmentName:assignmentName,
            timeLimit:timeLimit,
            questionIDs: questionIDs,
            deadline:deadline,
            tutGrp:tutGrp
        });
        for (i = 0; i < questionIDs.length; i++) {
            console.log("i:",i);
            console.log(req.body[i+1]);
            const { questionID, body, wrongOptions, correctOption, points } = req.body[i+1];
            const assignmentQ = new AssignmentQn({
                assignmentID:assignmentID,
                questionID:questionID,
                body: body,
                wrongOptions:wrongOptions,
                correctOption:correctOption,
                points:points,
            });
    
        const students = await Student.find({tutGrp:tutGrp});
        }
    }
    catch {
        res.status(400).send('There was an error in adding Assignment or Assignment Questions');
    }

    try{
        const assignmentExists = await Assignment.findOne({ assignmentID: assignmentID});
        if(assignmentExists) 
        {
            console.log("One");
            return res.status(409).send('This assignment already exists');
        }

    }
    catch(error){
        console.log("Two");
        res.status(400).send(error); //error checking using try catch
    }


    const assignment = new Assignment({
        assignmentID:assignmentID,
        assignmentName:assignmentName,
        timeLimit:timeLimit,
        questionIDs: questionIDs,
        deadline:deadline,
        tutGrp:tutGrp
    });
    assignment.save().then((result)=>{
        console.log(result);})
         .catch((err)=>{
             console.log(err);
             console.log("Three");
         res.status(400).send(err);});

    console.log("questionLength", questionIDs.length);
    for (i = 0; i < questionIDs.length; i++) {
        const { questionID, body, wrongOptions, correctOption, points } = req.body[i+1];
        const assignmentQ = new AssignmentQn({
            assignmentID:assignmentID,
            questionID:questionID,
            body: body,
            wrongOptions:wrongOptions,
            correctOption:correctOption,
            points:points,
        });
        assignmentQ.save().then((result)=>{
            console.log(result);})
                .catch((err)=>{
                    console.log("Four");
                    console.log(err);
                res.status(400).send(err);});
    }

    const students = await Student.find({tutGrp:tutGrp});
    const studList = [];
    for (i=0; i < students.length; i++) {
        const matricNo = students[i].matricNo;
        const firstName = students[i].firstName;
        const lastName = students[i].lastName;
        console.log(matricNo, firstName, lastName, tutGrp, assignmentID);

        const studentScore = new StudentScore({
            matricNo:matricNo,
            firstName:firstName,
            lastName:lastName,
            tutGrp:tutGrp
        })
        console.log(studentScore);
        studList.push(studentScore);
    }
    console.log(studList)
    const assignmentScore = new AssignmentScore({
        assignmentID:assignmentID,
        studentScoreDict:studList
    })
    assignmentScore.save().then((result)=>{
        console.log(result);})
         .catch((err)=>{
             console.log(err);
             console.log("Three");
         res.status(400).send(err);});

    return res.status(200).send("The values were added to the database");
}


//exporting the functions
module.exports={
    assignmentDetails,
    assignmentList,
    newAssignment,
};