//importing modules
const express=require('express');

//importing controllers
const assignmentControllers=require('../controllers/assignmentControllers');
const auth=require('../controllers/authentication');
//express router
const router=express.Router();

//routing
router.get('/list/:tut_grp',auth, assignmentControllers.assignmentList);
router.get('/:a_id',auth, assignmentControllers.assignmentDetails);
router.post('/:a_id',auth, assignmentControllers.newAssignment);

//exporting router
module.exports=router;
