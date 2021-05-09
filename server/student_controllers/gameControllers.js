//const StudentProgress = require('../models/studentProgress');
const studentProgress = require('../models/studentProgress');
const student = require('../models/student');

const unlockUniverse = async(req,res)=>{  
        
        studentProgress.updateOne({ emailID: req.body.emailID }, {
            conqueredUniverse: req.body.conqueredUniverse,
            conqueredSolarSystem: req.body.conqueredSolarSystem,
            conqueredPlanet: req.body.conqueredPlanet,
        }, function(
            err,
            result
        ) {
            if (err) {
            res.status(500).send(err);
            }
            else{
                res.status(200).send("Successful update");
            }
        });


        
        
};

const endGame = async(req,res)=>{

    var finalMessage = true;
    console.log("entering function")
    //updating fullDict 
        studentProgress.update(
            {
                emailID: req.body.emailID,
                "fullDict.identifier": `(${req.body.universe},${req.body.SolarSystem},${req.body.planet})`,
                
            },
            {$max:{
                "fullDict.$.maxScore": req.body.score,
                "fullDict.$.maxCorrect": req.body.correctAnswers,
            },
            $min:   {
                "fullDict.$.minScore": req.body.score,
            },
            $inc:{
                "fullDict.$.attempts": 1,
                "fullDict.$.totalScore": req.body.score,
            },

        },
        function(
            err,
            result
        ) {
            if (result.nModified == 0 && finalMessage) {
                console.log("print1")
                finalMessage = false
                res.status(500).send("Update error in studentProgress")
            }
            else{
                finalMessage = true
            }
        }
        );



        //updating SolarSystemDict
        studentProgress.update(
            {
                emailID: req.body.emailID,
                "solarSystemDict.identifier": `(${req.body.universe},${req.body.SolarSystem})`,
                
            },
            {$max:{
              
                "solarSystemDict.$.maxScore": req.body.score,
               
            },
            $min:   {
                "solarSystemDict.$.minScore": req.body.score,
            },
            $inc:{
                
                "solarSystemDict.$.attempts": 1,
                "solarSystemDict.$.totalScore": req.body.score,
                
            },

        },
        function(
            err,
            result
        ) {
            if (result.nModified == 0 && finalMessage) {
                console.log("print2")
                finalMessage = false
                res.status(500).send("Update error in studentProgress")
            }
            else{
                finalMessage = false
            }
        });


        // updating universeDict
        studentProgress.update(
            {
                emailID: req.body.emailID,
                
                "universeDict.identifier": `(${req.body.universe})`
                
            },
            {$max:{
                
                 "universeDict.$.maxScore": req.body.score,
            },
            $min:   {
                
                "universeDict.$.minScore": req.body.score
            },
            $inc:{
                "universeDict.$.attempts": 1,
                "universeDict.$.totalScore": req.body.score
            },

        },
        function(
            err,
            result
        ) {
            if (result.nModified == 0 && finalMessage) {
                console.log("print3")
                finalMessage = false
                res.status(500).send("Update error in studentProgress")
            }
            else{
                finalMessage = false
            }
        });


        // updating student schema
        var date = new Date();
       const dateExist = await student.exists({
           emailID: req.body.emailID,
           "scoreHistory.sysDate": date.toISOString().slice(0,10)
       });
       console.log(dateExist);
       //console.log(date.toISOString().slice(0,10));
       if (dateExist){
           student.update(
               {
                   emailID: req.body.emailID,
                   "scoreHistory.sysDate": date.toISOString().slice(0,10)
               },
               {
                   $set:{
                       totalScore: req.body.totalScore,
                   },
                   $inc:{
                    "scoreHistory.$.dailyScore": req.body.score
                   }
               },
               function(
                err,
                result
            ) {
                if (result.nModified == 0) {
                    //console.log("print")
                    if (finalMessage)
                        res.status(500).send("Update error in student")
                    else
                        finalMessage = false
                }
                else{
                    finalMessage = true;
                   
                    res.status(200).send('Success');
                }
            }
           );
       }
       else{
        student.update(
            {
                emailID: req.body.emailID,
            },
            {
                $set:{
                    totalScore: req.body.totalScore,
                },
                $push:{
                 scoreHistory:{
                     dailyScore: req.body.score,
                     "sysDate": date.toISOString().slice(0,10)
                 }
                }
            },
            function(
             err,
             result
         ) {
             if (result.nModified == 0) {
                //console.log("print")
                 if (finalMessage)
                     res.status(500).send("Update error in student")
                 else
                     finalMessage = false
             }
             else{
                finalMessage = true;
               
                res.status(200).send('Success');
             }
         }
        );
       }
}




module.exports={
    unlockUniverse,
    endGame
};