//importing modules
const express=require('express');
const auth=require('../controllers/authentication');
//importing controllers
const detailsControllers=require('../student_controllers/detailsControllers');

//express router
const router=express.Router();

//routing
// route called in the beginning after login for basic student details
router.get('/getStudent',auth,detailsControllers.getStudent);
//route called for progress bar
router.get('/getProgress',auth, detailsControllers.getProgress);
router.get('/getMaxScore',auth, detailsControllers.getMaxScore);
router.get('/getAllStudents/:tut_grp',auth, detailsControllers.getTutStudents);
router.get('/getAllStudents',auth, detailsControllers.getAllStudents);
router.get('/getLeaderboard',auth,detailsControllers.getLeaderboard);
router.patch('/updateStudent',auth, detailsControllers.updateStudent);
router.patch('/changePassword',auth, detailsControllers.changePassword);

//exporting router
module.exports=router;
