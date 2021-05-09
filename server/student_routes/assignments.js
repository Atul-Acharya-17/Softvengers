//importing modules
const express=require('express');
const auth=require('../controllers/authentication');
//importing controllers
const assignmentControllers=require('../student_controllers/assignmentControllers');

//express router
const router=express.Router();

//routing
router.patch('/assignmentComplete',auth, assignmentControllers.assignmentComplete);
router.get('/getassignmentList',auth, assignmentControllers.getassignmentList);
router.get('/getAssignmentQuestions',auth, assignmentControllers.getAssignmentQuestions);
//exporting router
module.exports=router;