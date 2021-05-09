//importing modules
const express=require('express');

//importing controllers
const studCreationControllers=require('../controllers/studCreationControllers');
const auth=require('../controllers/authentication');

//express router
const router=express.Router();

//routing
router.post('/',auth, studCreationControllers.studentCreation);


//exporting router
module.exports=router;