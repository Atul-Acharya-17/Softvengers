const mongoose=require('mongoose');

const Schema=mongoose.Schema;

const discussionSchema = new Schema({
    emailID:{
        type: String,
        required: true,
        default: "Anonymous"
    },
    text: {
        type: String,
        required: true
    },

    timing: {
        type: Date,
        default: Date.now()
    }
});

const Post = mongoose.model('Post', discussionSchema);
module.exports = Post;