const mongoose=require('mongoose');

const Schema=mongoose.Schema;

//defining the constants
//const MIN_QUES=5;

//defining subschemas
const studentTaker=new Schema({
    emailID:{
        type:String,
        required:true,
    },
    attempted:{
        type:Number,
        required:true,
        default:0,
    },
    score:{
        type:Number,
        required:true,
        min:0,
    },
    timeTaken:{
        type:Number,
        required:true,
    }
});

const topic=new Schema({
    universe:{
        type: Number,
        required:true,
    },
    solarSystem:{
        type: Number,
        required:true,
    },
    planet:{
        type: Number,
        required:true,
    },
    noOfQuestions:{
        type:Number,
        required:true,
        min:1
    },
});

//creating the schema for challenges in the database
const challengeSchema=new Schema({
    // challengeId:{
    //     unique: true,
    //     type: Number,
    //     index: true,
    //     required: true,
    //     min:1,
    // },
    challengeName:{
        type:String,
        required:false,
        //default: "Ultimate Challenge"
    },
    questionIds:{
        type:[Number],
        required:true,
        // validate: {validator: function (v) {
        //     return v.length>=MIN_QUES}} //validating array length
    },
    sender:{
        type: studentTaker,
        required:true,
    },
    receivers:{
        type:[studentTaker],
        required:true,
    },
    questionTopics:{
        type:[topic],
        required:true,
    }
    
});

//making the mongoose model and exporting it
const Challenge=mongoose.model('Challenge',challengeSchema);

const student = mongoose.model('studentTaker', studentTaker)

module.exports=
{ 
    Challenge, 
    student

}