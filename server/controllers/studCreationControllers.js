/*
* controller for student account creation
*/
const Student=require('../models/student');
const StudentProgress=require('../models/studentProgress');
const UniSolarCount=require('../models/UniSolarCount');
const bcrypt = require('bcrypt');
const nodemailer=require('nodemailer');

//defining constants
const saltRounds=10;


const studentCreation= async (req,res)=>{
    const { emailID, password, matricNo, firstName, lastName, tutGrp}=req.body;

    //checking if the student already exists
    try{
        const studentExists = await Student.findOne({ emailID: emailID});
        if(studentExists) 
        {
            return res.status(409).send('This student already exists');
        }

    }
    catch(error){
        res.status(400).send(error); //error checking using try catch
    }
    let uniDict=[];
    let solarDict=[];
    let planetDict=[];
    let noOfUniverse=await UniSolarCount.count();
    let noOfPlanet=3;
    for(let i=0;i<noOfUniverse;i++)
    {   const universeID=await UniSolarCount.findOne({universeID:i});
        let noOfSolarSystem=universeID.noOfSolar;
        for(let j=0;j<noOfSolarSystem;j++)
        {   
            for(let k=0;k<noOfPlanet;k++)
            {   let pID="("+i.toString()+","+j.toString()+","+k.toString()+")";
                let objP={
                    identifier:pID,
                    maxScore:0,
                    minScore:100,
                    totalScore:0,
                    attempts:0,
                }
                planetDict.push(objP);
            }
            let sID="("+i.toString()+","+j.toString()+")";
                let objS={
                    identifier:sID,
                    maxScore:0,
                    minScore:100,
                    totalScore:0,
                    attempts:0,
                }
                solarDict.push(objS);
        }
        let uID="("+i.toString()+")";
        let objU={
            identifier:uID,
            maxScore:0,
            minScore:100,
            totalScore:0,
            attempts:0,
        }
        uniDict.push(objU);
    }
    console.log(planetDict);
    //creating StudentProgress
    const studentProgress=new StudentProgress({
            emailID:emailID,
            conqueredUniverse:0,
            conqueredSolarSystem:0,
            conqueredPlanet:0,
            fullDict:planetDict,
            solarSystemDict:solarDict,
            universeDict:uniDict,

    });
    studentProgress.save().then((result)=>{
        console.log(result);})
         .catch((err)=>{
             console.log(err);
         res.status(400).send(err);});


    //hashing the password
    bcrypt.genSalt(saltRounds, function(error, salt) {
        bcrypt.hash(password, salt, function(error, hash) {
        // Store hash in database here
        const student = new Student({
            emailID: emailID,
            password: hash,
            matricNo:matricNo,
            firstName:firstName,
            lastName:lastName,
            tutGrp:tutGrp
        });
        student.save().then((result)=>{
            console.log(result);
            sendEmail(req,res);
             res.status(200).send(result)})
             .catch((err)=>{
                 console.log(err);
             res.status(400).send(err);});
        });
      });

};


const sendEmail = (req,res)=>{
    const { emailID, password, matricNo, firstName, lastName}=req.body;
    // create reusable transporter object using the default SMTP transport\
const transporter = nodemailer.createTransport({
    port: 465,               // true for 465, false for other ports
    host: "smtp.gmail.com",
       auth: {
            user: process.env.HOST_EMAIL,
            pass: process.env.HOST_PASSWORD,
         },
    secure: true,
    });

    const mailData = {from: process.env.HOST_EMAIL,  // sender address
    to: emailID,   // list of receivers
    subject: 'Student Credentials for Softvengers game',
    text: 'Hello from team Softvengers, '+firstName +" "+lastName+". You have been signed up for the Softvengers game. Please sign in using this email ID and the password: "+password,
    //html: '<b>Hey there! </b><br> This is our first message sent with Nodemailer<br/>',
  };

  transporter.sendMail(mailData,(err,info)=>{
    if(err){
        console.log(err);
    }
    else
    {
        console.log(info);
        res.status(200).send('Student has been added');
    }
})
 
};

//exporting the functions
module.exports={
    studentCreation
};