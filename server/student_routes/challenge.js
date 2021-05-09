//importing modules
const express=require('express');
const auth=require('../controllers/authentication');
//importing controllers
const challengeControllers=require('../student_controllers/challengeControllers');

//express router
const router=express.Router();

//routing
router.post('/createChallenge',auth,challengeControllers.createChallenge);
router.patch('/sendChallenge',auth, challengeControllers.sendChallenge);
router.get('/getReceivedChallenges', auth,challengeControllers.getReceivedChallenges);
router.get('/getSentChallenges',auth,challengeControllers.getSentChallenges);
router.patch('/endChallenge',auth,challengeControllers.attemptChallenge);
router.get('/getQuestions',auth,challengeControllers.getQuestions);
router.patch('/declineChallenge',auth,challengeControllers.declineChallenge);

//exporting router
module.exports=router;