const mongoose = require('mongoose');
const Schema = mongoose.Schema;

//defining the limiting consts
const MIN_SCORE=0;
const MIN_VOL=0;
const MAX_VOL=100;
const MIN_PASS_LEN=10;
const MAX_PASS_LEN=100;
const MIN_TUT=2;
const MAX_TUT=4;

//defining the scoreHistory sub-schema
const scoreHistory=new Schema({
    sysDate:{
        type:Date,
        required:true,
        default: Date.now,
    },
    dailyScore:{
        type: Number,
        required:true,
        default:0,
        min:MIN_SCORE,
    },
});

//defining the database schema for a student
const studentSchema= new Schema({
    emailID:{
        type: String,
        unique: true,
        index: true,
        required: true,
    },
    password:{
        type: String,
        required: true,
        min: MIN_PASS_LEN,
        max: MAX_PASS_LEN,
    },
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
    tutGrp:{
        type: String,
        required: true,
        index: true,
        min:MIN_TUT,
        max:MAX_TUT
    },
    volume:{
        type: Number,
        required: true,
        default: 0,
        min:MIN_VOL,
        max:MAX_VOL,
    },
    avatar:{
        type: Number,
        required: true,
        default: 0,
    },
    totalScore:{
        type: Number,
        required: true,
        default:0,
        min: MIN_SCORE,
    },
    scoreHistory:{
        type: [scoreHistory],
        required:true,
        default: {}
    }

});

const Student= mongoose.model('Student', studentSchema);
module.exports=Student;