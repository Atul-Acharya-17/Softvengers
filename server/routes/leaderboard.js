//importing modules
const express=require('express');

//importing controllers
const leaderboardControllers=require('../controllers/leaderboardControllers');
const auth=require('../controllers/authentication');
//express router
const router=express.Router();

//routing
router.get('/',auth, leaderboardControllers.fullLeaderboard);
router.get('/:tut_gp',auth, leaderboardControllers.tutLeaderboard);

//exporting router
module.exports=router;