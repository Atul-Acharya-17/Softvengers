const mongoose = require('mongoose');
const Schema = mongoose.Schema;

//defining constants
const MAX_SCORE=3000;
const MIN_SCORE=1;
const WRONG_OPTIONS=3;

  //defining the schema of assignment questions
const assignmentQnSchema = new Schema({
    questionID:{
       // unique: true,
        type: Number,
        required: true,
        min:1,
    },
    assignmentID:{
        type: Number,
        index: true,
        required: true,
        min:1,
    },
    body:{
        type: String,
        required:true,
    },
    wrongOptions:{
        type: [String],
        required: true,
        validate: {validator: function (v) {
            return v.length===WRONG_OPTIONS}} //validating array length
    },
    correctOption:{
        type: String,
        required: true,
    },
    points:{
        type: Number,
        required: true,
        default: 1,
        min: MIN_SCORE,
        max: MAX_SCORE,

    },
});

//making the mongoose model and exporting it
const AssignmentQn= mongoose.model('AssignmentQuestion', assignmentQnSchema);
module.exports=AssignmentQn;