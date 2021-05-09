//importing modules
const express=require('express');
const auth=require('../controllers/authentication');
//importing controllers
const gameControllers=require('../student_controllers/gameControllers.js');

//express router
const router=express.Router();

//routing
router.patch('/unlock',auth,gameControllers.unlockUniverse);
router.patch('/endGame',auth,gameControllers.endGame);
//exporting router
module.exports=router;