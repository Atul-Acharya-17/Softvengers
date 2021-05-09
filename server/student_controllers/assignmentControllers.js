const Assignment=require('../models/assignment');
//const AssignmentScore = require('../models/assignmentScore');
const AssignmentQn = require('../models/assignmentQuestion');
const Student = require('../models/student');
const schemas = require('../models/assignmentScore');
//const StudentScore = schemas.StudentScore;
const AssignmentScore = schemas.AssignmentScore;

//function to send back a list of assignments
const getassignmentList=async (req,res)=>{
    
    const myDetails = await AssignmentScore.find({
        'studentScoreDict.matricNo': req.query.matricNo
    },
    {
        'studentScoreDict.$':1,
        _id : 0
    });

    const merge = await Assignment.aggregate([
        {
            $lookup:
            {
                from: AssignmentScore.collection.name,
                localField: "assignmentID",
                foreignField: "assignmentID",
                as: "fromScores"
            }
        },
            {
                $replaceRoot: { newRoot: { $mergeObjects: [ { $arrayElemAt: [ "$fromScores", 0 ] }, "$$ROOT" ] } }
             },
             { $project: { fromScores: 0 } },
             { "$match": {'studentScoreDict.matricNo': req.query.matricNo}}
            
        
    ])
    //console.log (merge);
    //console.log(assigndetails);

    let finalMessage = JSON.parse(JSON.stringify(merge));
    finalMessage.map((assign, row)=>{
        //console.log(myDetails[row].studentScoreDict[0].scores);
        //console.log(myDetails[row].studentScoreDict[0].attemptStatus);
        assign['myScore'] = myDetails[row].studentScoreDict[0].scores;
        assign['myStatus'] = myDetails[row].studentScoreDict[0].attemptStatus;
        delete assign['questionIDs'];
        delete assign['studentScoreDict'];
        delete assign['__v'];

    })
    //console.log(finalMessage);
    res.status(200).send(finalMessage);
}

const getAssignmentQuestions = async (req,res) =>{
    try{
    const questions = await AssignmentQn.find({
        "assignmentID":req.query.assignmentID
        
    });
    console.log(questions);
    res.status(200).send(questions);}
    catch{
        res.status(400).send(err);
    }
}


const assignmentComplete = async(req,res) =>{

    AssignmentScore.update({
        assignmentID: req.body.assignmentID,
        "studentScoreDict.matricNo": req.body.matricNo
    },
    {
        $set:{
            "studentScoreDict.$.scores": req.body.scores,
            "studentScoreDict.$.attemptStatus": true,
        }
    },
    function(
        err,result
    ) {
        if (err) {
            res.status(500).send(err);
        }
        else{
            res.status(200).send(result);
        }
    }
    )

};

module.exports={
    assignmentComplete,
    getassignmentList,
    getAssignmentQuestions
};

// updategetAsssignmentList
