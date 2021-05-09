/*
* This file has controllers to deal with teacher login
*/

//imports 
const Teacher = require('../models/teacher');
const UniSolarCount=require('../models/UniSolarCount');
const bcrypt = require('bcrypt');
const jwt = require('jsonwebtoken');

//setting constants
const accessTokenSecret=process.env.TOKEN;
const saltRounds=10;

//logic for teacher login
const teacherLogin=async(req,res)=>{
    console.log('teacher login in progress');
    const {emailID,password}=req.body;
      //validating the username and password
      const emailExists = await Teacher.findOne({emailID: emailID});
      if(emailExists)
      {
          const passwordCheck = await bcrypt.compare(password, emailExists.password);
          if(passwordCheck)
          {
           //generating and returning the token
           const token = jwt.sign({ email: emailExists.emailID,  id:emailExists._id , tutGp:emailExists.tutGrp}, accessTokenSecret, {expiresIn: '1d'});   
           res.status(200).header('token', token).send(token);   
          }
          else
          {
              return res.status(404).send("Password is incorrect for the user");
          }
         
      }
      else
      {
          return res.status(404).send('Username does not exist');
      }
      
  

};


//logic for teacher registration
const teacherRegister=async (req,res)=>{
    console.log('teacher registration in progress');
    const { emailID, password, firstName, lastName, tutGrp } = req.body;

    //checking if teacher already exists
    try{
        const teacherExists = await Teacher.findOne({ emailID: emailID});
        if(teacherExists) 
        {
            return res.status(409).send('This teacher already exists');
        }

    }
    catch(error){
        res.status(400).send(error); //error checking using try catch
    }

    //hashing the password
    bcrypt.genSalt(saltRounds, function(error, salt) {
        bcrypt.hash(password, salt, function(error, hash) {
        // Store hash in database here
        const teacher = new Teacher({
            emailID:emailID,
            password: hash,
            firstName:firstName,
            lastName:lastName,
            tutGrp:tutGrp,
        });
        teacher.save().then((result)=>{
            console.log(result);
             res.status(200).send(result);})
             .catch((err)=>{
                 console.log(err);
             res.status(400).send(err);});
        });
      });


};

//logic for initialisation of universes with solar systems
const initialisation=async(req,res)=>{
    const {universeID, noOfSolar}=req.body;

    //checking if this universeID already exists
    try{
    const uniExists=await UniSolarCount.findOne({universeID:universeID});
    if(uniExists)
    {
        res.status(409).send('This universe data already exists in the database');
    }
    else
    {
        let UniSolar=new UniSolarCount({
            universeID:universeID,
            noOfSolar:noOfSolar
        });
        UniSolar.save().then((result)=>{
            console.log(result);
             res.status(200).send(result);
        })
        .catch((err)=>{
            console.log(err);
            res.status(400).send(err);
        })
    }
}
catch(err)
{
    res.status(400).send(err);
}
};

//exporting the functions
module.exports={
    teacherLogin,
    teacherRegister,
    initialisation
};