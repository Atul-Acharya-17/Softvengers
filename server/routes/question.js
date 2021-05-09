//importing modules
const express=require('express');

//importing controllers
const questionControllers=require('../controllers/questionControllers');
const auth=require('../controllers/authentication');

//express router
const router=express.Router();

//routing
router.get('/',auth, questionControllers.questionList);
router.patch('/populate',auth,questionControllers.populate);
router.get('/:q_id', auth, questionControllers.questionDetails);
router.post('/:q_id', questionControllers.newQuestion);
router.put('/:q_id',auth, questionControllers.updateQuestion);
router.delete('/:q_id', auth, questionControllers.deleteQuestion);


//exporting router
module.exports=router;