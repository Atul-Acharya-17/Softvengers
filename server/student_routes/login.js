//importing modules
const express=require('express');

//importing controllers
const loginControllers=require('../student_controllers/loginControllers');

//express router
const router=express.Router();

//routing
router.post('/login',loginControllers.studentLogin);

//exporting router
module.exports=router;