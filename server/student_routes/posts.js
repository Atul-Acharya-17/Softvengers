const express = require('express');
const auth=require('../controllers/authentication');
const postControllers = require('../student_controllers/discussionControllers');

const router=express.Router();

router.post('/createPost',auth, postControllers.createPost);
router.get('/getPosts',auth, postControllers.getPosts);

module.exports = router;
