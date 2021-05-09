const questions = require('../models/planetQuestion');

const getQuestions = async(req,res)=>{
    // try{
        // questions.find({
        //     universeID: req.query.universe,
        //     solarID: req.query.solarSystem,
        // }).then((result)=>{
        //     if(result!=null)
        //     {  
        //         let planetQuestions=result.map(planetQ=>{ 
        //         let question={
        //             questionID:planetQ.questionID,
        //             body:planetQ.body,
        //             correctOption:planetQ.correctOption,
        //             wrongOptions:planetQ.wrongOptions,
        //             difficulty: planetQ.planetID
        //         }
        //          return question; });
        //     if("[]"=== JSON.stringify(planetQuestions))
        //     {
        //         res.status(400).send('Universe or SS does not exist');
        //     }
        //     else
        //     {
        //     res.status(200).json({questions: JSON.parse(JSON.stringify(planetQuestions))});
        //     }
        //     }
        //     else
        //     {
        //         res.status(400).send('Universe or SS does not exist');
        //     }
        // })
        const easy = await questions.find({
            universeID: req.query.universe,
            solarID: req.query.solarSystem,
            planetID: 0
        });
        const medium = await questions.find({
            universeID: req.query.universe,
            solarID: req.query.solarSystem,
            planetID: 1
        });
        const hard = await questions.find({
            universeID: req.query.universe,
            solarID: req.query.solarSystem,
            planetID: 2
        });

        res.status(200).json({
            'easy': JSON.parse(JSON.stringify(easy)),
            'medium':JSON.parse(JSON.stringify(medium)),
            'hard': JSON.parse(JSON.stringify(hard))
        })
    // catch(err){
    //     res.status(404).send("Universe or solar system does not exist")
    // }
}

module.exports={
    getQuestions,
}