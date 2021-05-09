const mongoose = require('mongoose');
const Schema = mongoose.Schema;

//defining the limiting consts
const MIN_PASS_LEN=10;
const MAX_PASS_LEN=100;
const MIN_TUT=2;
const MAX_TUT=4;

//defining the database schema for a teacher
const teacherSchema= new Schema({
    emailID:{
        type: String,
        unique: true,
        index: true,
        required: true
    },
    password:{
        type: String,
        required: true,
        min:MIN_PASS_LEN,
        max:MAX_PASS_LEN
    },
    firstName:{
        type: String,
        required: true
    },
    lastName:{
        type: String,
        required: true
    },
    tutGrp:{
        type: String,
        required: true,
        index: true,
        min:MIN_TUT,
        max:MAX_TUT
    }
});

const Teacher= mongoose.model('Teacher', teacherSchema);
module.exports=Teacher;