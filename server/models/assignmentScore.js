const mongoose = require('mongoose');
const Schema = mongoose.Schema;

//defining the limiting constants
const MIN_ASSN_SCORE=0;

//defining the subschema that stores scores of each student who took the assignment
const studentScores=new Schema({
    matricNo:{
        type: String,
        required: true,
        length:[9,"Matriculation number can have 9 characters only."]
    },
    firstName:{
        type: String,
        required: true,
    },
    lastName:{
        type: String,
        required: true,
    },
    attemptStatus:{
        type:Boolean,
        required:true,
        default:false,
    },
    scores:{
        type: Number,
        required:true,
        default:0,
        min:MIN_ASSN_SCORE,
    }
})

//main schema for storing assignment score report
const assnmtScoreSchema=new Schema({
    assignmentID:{
        unique: true,
        type: Number,
        index: true,
        required: true,
        min:1,
    },
    studentScoreDict:{
        type:[studentScores],
        required:true,
        default:[],
    }
});

//making the mongoose model and exporting it
const AssignmentScore= mongoose.model('AssignmentScore', assnmtScoreSchema);
const StudentScore = mongoose.model('StudentScore', studentScores);
module.exports={StudentScore, AssignmentScore};