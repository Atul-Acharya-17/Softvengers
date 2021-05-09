/*
* deals with controllers for leaderboard
*/
const Student=require('../models/student')

//function that returns all the students sorted based on their score
const fullLeaderboard=async(req,res)=>{
       Student.find()    
        .then((result)=>{
            console.log(result);
            if(result!=null)
            {let leaders=result.map((leader)=>{ 
                let lead={
                    name:leader.firstName + ' ' + leader.lastName,
                    emailID:leader.emailID,
                    totalScore:leader.totalScore.toFixed(3),
                    tutGrp:leader.tutGrp
                }
                    return lead; });
                if("[]"=== JSON.stringify(leaders))
                    {
                        res.status(400).send('There are currently no students in the database');
                    }
                    else{
            res.status(200).send(leaders);
                    }
            }
            else
            {
                res.status(400).send('There are no students in leaderboard');
            }

            //return res.status(200).send(StudentArray);
        })
        .catch ((err)=>{
            res.status(400).send(err); //error checking using try catch
        });
};

//function that returns students of a particular tutorial group sorted based on their score
const tutLeaderboard=async(req,res)=>{
    console.log(req.params.tut_gp);
    const tutGrp = req.params.tut_gp;
    Student.find({tutGrp: tutGrp}).then((result)=>{
        console.log(result);
        if(result!=null)
        {let leaders=result.map((leader)=>{ 
            let lead={
                name:leader.firstName + ' ' + leader.lastName,
                emailID:leader.emailID,
                totalScore:leader.totalScore.toFixed(3),
                tutGrp:tutGrp
            }
                return lead; });
                if("[]"=== JSON.stringify(leaders))
                    {
                        res.status(400).send('There are currently no students in the database');
                    }
                    else{
        res.status(200).send(leaders);
                }
        }
        else
        {
            res.status(400).send('There are no students in leaderboard');
        }
       
    })
    .catch ((err)=>{
        res.status(400).send(err); //error checking using try catch
    });
};

module.exports={
    fullLeaderboard,
    tutLeaderboard
}