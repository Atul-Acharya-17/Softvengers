//importing modules
const express=require('express');
const auth=require('../controllers/authentication');
//importing controllers
const questionControllers=require('../student_controllers/questionController');

//express router
const router=express.Router();

//routing
router.get('/',auth,questionControllers.getQuestions);

//exporting router
module.exports=router;