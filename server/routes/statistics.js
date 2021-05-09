//importing modules
const express=require('express');

//importing controllers
const statsControllers=require('../controllers/statsControllers');
const auth=require('../controllers/authentication');

//express router
const router=express.Router();

//routing
router.get('/group/:tut_id',auth, statsControllers.groupStats);
router.get('/:student_id',auth, statsControllers.indivStats);

//exporting router
module.exports=router;