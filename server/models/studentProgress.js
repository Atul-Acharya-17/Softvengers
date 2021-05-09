

//creating the schema for Student Progress in the database
const mongoose=require('mongoose');

const Schema=mongoose.Schema;

const fullDict = new Schema({
    identifier:{
    //"(0,0,0)"
        type: String,
        required:true
    },
    maxScore: {
        type: Number,
        min: 0,
        max: 100,
        required: true
    },
    minScore: {
        type: Number,
        min: 0,
        max: 100,
        required: true
    },
    totalScore: {
        type: Number,
        min: 0,
        max: 100,
        required: true
    },
    attempts: {
        type: Number,
        required: true
    },
    maxCorrect:{
        type: Number,
        min: 0,
        default:0,
        required: true
    }
});

const solarSystemDict = new Schema({
    identifier:{
        //"(0,0)"
        type: String,
        required:true
    },
    maxScore: {
        type: Number,
        min: 0,
        max: 100,
        required: true
    },
    minScore: {
        type: Number,
        min: 0,
        max: 100,
        required: true
    },
    totalScore: {
        type: Number,
        min: 0,
        max: 100,
        required: true
    },
    attempts: {
        type: Number,
        required: true
    },

});

const universeDict = new Schema({
    identifier:{
        //"0"
        type: String,
        required:true
    },
    maxScore: {
        type: Number,
        min: 0,
        max: 100,
        required: true
    },
    minScore: {
        type: Number,
        min: 0,
        max: 100,
        required: true
    },
    totalScore: {
        type: Number,
        min: 0,
        max: 100,
        required: true
    },
    attempts: {
        type: Number,
        required: true
    },
   
});

//creating the schema for Student Progress in the database
const studentProgress=new Schema({
    emailID:{
        unique: true,
        type: String,
        required: true
    },
    conqueredUniverse:{
        type: Number,
        default: 0,
        min: 0,
        required:true
    },
    conqueredSolarSystem:{
        type: Number,
        default: 0,
        min: 0,
        required:true
    },
    conqueredPlanet:{
        type: Number,
        default: 0,
        min: 0,
        required:true
    },
    fullDict:[fullDict],
    solarSystemDict: [solarSystemDict],
    universeDict:[universeDict],
});

//making the mongoose model and exporting it
const StudentProgress=mongoose.model('StudentProgress',studentProgress);
module.exports=StudentProgress;