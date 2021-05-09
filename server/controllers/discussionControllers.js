const Post = require('../models/discussion');

const getPosts = async(req,res) => {
    try{
        const posts = await Post.find().sort({'timing': -1});
        delete posts['timing'];
        delete posts['_id'];
        delete posts['__v'];
        res.status(200).json(posts.slice(0,100));
    }
    catch (err) {
        res.status(400).send(err);
    }
}

const createPost = async(req, res) => {
    const { emailID, text } = req.body;
    const post = new Post({
        emailID: emailID,
        text: text,
    });
    try{
        await post.save();
        res.status(201).json("Received");
    }
    catch(err){
        res.status(400).send(err);
    }
}

module.exports = {
    getPosts,
    createPost
}