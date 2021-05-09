const Student = require('../models/student');
const StudentProgress = require('../models/studentProgress');
const bcrypt = require('bcrypt');

const getStudent = async(req,res)=>{
    try{
        //get Student details
        const stud = await Student.findOne({emailID: req.query.emailID});
        console.log(req.query);
        const sp = await StudentProgress.findOne({emailID: req.query.emailID});
        let sp_pseudo = JSON.parse(JSON.stringify(sp));
        let pseudo = JSON.parse(JSON.stringify(stud));


        // delete items not required
        delete pseudo['password'];
        delete pseudo['__v'];
        delete pseudo['scoreHistory'];

        // add items required
        pseudo['conqueredUniverse'] = sp_pseudo['conqueredUniverse'];
        pseudo['conqueredSolarSystem'] = sp_pseudo['conqueredSolarSystem'];
        pseudo['conqueredPlanet'] = sp_pseudo['conqueredPlanet'];

        // send details

        res.setHeader('Content-Type', 'application/json');
        res.json(pseudo);

    }
    catch(err){
        res.status(404).json({message:"Student does not exist in DB"});

    }
};

const getProgress = async(req,res)=>{
    try{
        //get student details and full dict identifier
        const resultobj = await StudentProgress.findOne(
            {emailID: req.query.emailID,
            //"fullDict.identifier":`(${req.query.universe},${req.query.SolarSystem},${req.query.planet})` 
        });
        console.log(resultobj['fullDict']);
        let result = resultobj['fullDict'].map((level)=>{
            let lev = {
                identifier: level.identifier,
                maxCorrect: level.maxCorrect
            }
            return lev;
        })
        res.status(200).json({
            "progress": result
        });

    }
    catch(err){
        res.status(404).send("Invalid emailID");
    }
};

const getMaxScore = async(req,res)=>{
    try{
        //get student details and full dict identifier
        const resultobj = await StudentProgress.findOne(
            {emailID: req.query.emailID,
            "fullDict.identifier":`(${req.query.universe},${req.query.SolarSystem},${req.query.planet})` },
            {
                "fullDict.$":1,
                _id: 0
            });
        console.log(resultobj['fullDict'][0]['maxScore']);
        res.status(200).json(
            resultobj['fullDict'][0]['maxScore']
    );
    }
    catch(err){
        res.status(500).send("Invalid tuple");
    }
};

const getLeaderboard = async(req,res)=>{
    //try{
    const studentSorted = await Student.find().sort({'totalScore': -1});
    let rank = 0;
    let finalRank = 0;
    let final = studentSorted.map((student)=>{
        if (student.emailID == req.query.emailID){
            finalRank = rank;
        }
        else{
            rank++;

        }
        let result = {
            totalScore: student.totalScore,
            firstName: student.firstName,
            lastName: student.lastName,
            emailID: student.emailID,

        }
        return result;

    });
    console.log(finalRank);
    res.status(200).json({students: final.slice(0,9), myRank: finalRank+1});
//}
//catch(err){
   // res.status(400).send(err);
//}  
};


const updateStudent = async(req,res)=>{
    const studentExists = await Student.findOne({ emailID: req.body.emailID});
    if(!studentExists) 
    {
        return res.status(409).send('This student does not exist');
    }
    // const salt = await bcrypt.genSalt(10);
    // const hashedPass = await bcrypt.hash(req.body.password, salt)
    Student.update(
        {
            emailID: req.body.emailID,
        },
        {
            $set:{
                //password: hashedPass,
                volume: req.body.volume,
                avatar: req.body.avatar
            },
        },
        function(
         err,
         result
     ) {
         if (err) {
            return res.status(500).send("Update error in student")
         }
         else{
             return res.status(200).send('Success');
         }
     }
    );
};


const changePassword = async (req, res) => {
    const studentExists = await Student.findOne({ emailID: req.body.emailID});
    if (!studentExists)
    {
        return res.status(409).send('This student does not exist');
    }
    const salt = await bcrypt.genSalt(10);

    const passwordCheck = await bcrypt.compare(req.body.oldPassword, studentExists.password);
    if (passwordCheck)
    {
        const hashedPass = await bcrypt.hash(req.body.newPassword, salt);
        Student.update(
            {
                emailID: req.body.emailID,
            },
            {
                $set:
                {
                    password: hashedPass,
                },
            },
            function(
             err,
             result
            )  {
                    if (err)
                    {
                        return res.status(500).send("Update error in student")
                        }
                    else
                    {
                        return res.status(200).send('Success');
                    }
                }
        );
    }
    else{
        res.status(500).send('Failure')
    }

};

const getTutStudents = async(req,res) =>{

    var tut = req.params.tut_grp;
    var tutgrp = tut.toUpperCase();
    Student.find({
        tutGrp: tutgrp
    }).then((result)=>{
        if(result != null){

            let students =result.map((res)=>{
                let student = {
                emailID: res.emailID,
                name: `${res.firstName} ${res.lastName}`
                };
                return student;
            
            })

            res.status(200).send(JSON.parse(JSON.stringify(students)));
        }
        else{
            res.status(404).send('Tut Grp does not exist')
        }
    }
    )
};

const getAllStudents = async(req,res) =>{

    Student.find().then((result)=>{
        if(result != null){

            let students =result.map((res)=>{
                let student = {
                emailID: res.emailID,
                name: `${res.firstName} ${res.lastName}`
                };
                return student;
            
            })

            res.status(200).send(JSON.parse(JSON.stringify(students)));
        }
        else{
            res.status(404).send('Tut Grp does not exist')
        }
    }
    )
};





module.exports={
    getStudent,
    getProgress,
    getLeaderboard,
    getMaxScore,
    updateStudent,
    changePassword,
    getTutStudents,
    getAllStudents
};
