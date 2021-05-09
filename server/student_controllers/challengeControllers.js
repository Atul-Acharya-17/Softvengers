const schemas = require('../models/challenge');

const questionSchema = require('../models/planetQuestion');

const challengeSchema = schemas.Challenge;

const studentTaker = schemas.student;

const Student = require('../models/student');


const createChallenge = async (req,res) =>{
    try{
    const student = new studentTaker({
        emailID: req.body.senderEmailID,
        attempted: false,
        score: 0,
        timeTaken: 0
    });  
    // populate questionIDs
    const questionObj = [];
    const qid = [];
    var name;
    let username = req.body.senderEmailID.split('@')[0];
    if (!req.body.challengeName){
         name = `Challenge by ${username}`
    }
    else{
        name = req.body.challengeName;
    }
    const topics = req.body.topics;

    for (const topic of topics){
        let quests = await questionSchema.aggregate([
            { $match:{ 
                universeID: topic.universe,
                solarID: topic.solarSystem,
                planetID: topic.planet
            }
        } ,
        {
            $sample: {size: topic.noOfQuestions}
        }
    ]);
        quests.map((quest)=>{
            questionObj.push(quest);
            qid.push(quest.questionID);
        })
    }
    console.log(questionObj)
    
    //res.json(questionObj);
    console.log (qid);

    const challenge = new challengeSchema({
        challengeName: name,
        sender: student,
        questionIds: qid,
        receivers: [],
        questionTopics: req.body.topics,

    });

    
        const savedChallenge = await challenge.save();
        res.status(201).json({challengeID: savedChallenge.id,
        questions: questionObj});
    }
    catch(err){
        res.status(400).send(err);
    }


}

const sendChallenge = async(req,res) =>{

    const receivers = [];
    const req_receivers = req.body.receivers;

    req_receivers.map(
        (receiver)=>{
            const rec = new studentTaker({
                emailID: receiver,
                attempted: 0,
                score: 0,
                timeTaken: 0
            });
            receivers.push(rec);
        }
    )
    console.log(receivers);

    challengeSchema.update({
        _id: req.body.challengeID
    },
    {
        $set:{
            receivers: receivers,
            "sender.score": req.body.senderScore,
            "sender.timeTaken": req.body.senderTime,
            "sender.attempted":1
        }
    },
    function(
        err,result
    ) {
        if (err) {
            res.status(400).send(err);
        }
        else{
            res.status(200).send(result);
        }
    }
    )

};

const getReceivedChallenges = async (req, res) =>{
    
    const challenges = await challengeSchema.find({
        'receivers.emailID': req.query.emailID,
        'receivers.attempted': {
            $ne: -1
        }
    });

    console.log(challenges);
    const myDetails =await challengeSchema.find({
        'receivers.emailID': req.query.emailID,
        'receivers.attempted': {
            $ne: -1
        }
    },
    {
        'receivers.$': 1,
        _id: 0
    });
    console.log(myDetails);
    let myDet = JSON.parse(JSON.stringify(myDetails));

    console.log(myDet);

    let finalMessages = JSON.parse(JSON.stringify(challenges));  
    
    finalMessages.map((finalMessage, row)=>{
        delete finalMessage['receivers'];
        delete finalMessage['__v'];

        finalMessage['myTime'] = myDet[row].receivers[0].timeTaken;
        finalMessage['myScore'] = myDet[row].receivers[0].score;
        finalMessage['myStatus'] = myDet[row].receivers[0].attempted;
    });

    res.status(200).send(finalMessages);
}

const getSentChallenges = async (req, res) =>{
    
    //console.log(req.query.emailID);
    const challenges = await challengeSchema.find({
        'sender.emailID': req.query.emailID
    }).sort({'sender.score':-1});

    // const myDetails =await challengeSchema.find({
    //     'receivers.emailID': req.body.emailID
    // },
    // {
    //     'receivers.$': 1,
    //     _id: 0
    // });

    // let myDet = JSON.parse(JSON.stringify(myDetails));

    // console.log(myDet);

    let finalMessages = JSON.parse(JSON.stringify(challenges));  
    
    finalMessages.map((finalMessage)=>{
        delete finalMessage['__v'];
        finalMessage['myTime'] = finalMessage.sender.timeTaken;
        finalMessage['myScore'] = finalMessage.sender.score;
        finalMessage['myStatus'] = finalMessage.sender.attempted;
        delete finalMessage['sender'];
    });

    res.status(200).send(finalMessages);

};

const getQuestions = async(req,res)=>{
    const qids = await challengeSchema.findOne(
        {
            _id: req.query.challengeID
        }
    );
    console.log(qids.questionIds);
    const questions = await questionSchema.find({
        'questionID':{
            $in:qids.questionIds
        }
    })
    res.status(200).send(questions);
};


const attemptChallenge = async(req,res) =>{

    challengeSchema.update({
        _id: req.body.challengeID,
        "receivers.emailID": req.body.emailID
    },
    {
        $set:{
            "receivers.$.score": req.body.receiverScore,
            "receivers.$.timeTaken": req.body.receiverTime,
            "receivers.$.attempted":1
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

const declineChallenge = async(req,res) =>{
    challengeSchema.update({
        _id: req.body.challengeID,
        "receivers.emailID": req.body.emailID
    },
    {
        $set:{
            "receivers.$.attempted":-1
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
    createChallenge,
    sendChallenge,
    getReceivedChallenges,
    getSentChallenges,
    attemptChallenge,
    getQuestions,
    declineChallenge
};

// change attempt status to -1