const mongoose = require('mongoose');
const Schema = mongoose.Schema;

//defining constants
const MAX_SCORE=30;
const MIN_SCORE=1;
const WRONG_OPTIONS=3;
const MAX_UNI=5;
const MAX_PLAN=2;

  //defining the schema of Planet questions
const planetQnSchema = new Schema({
    universeID:{
        type:Number,
        required:true,
        min:0,
        max:MAX_UNI,
    },
    solarID:{
        type:Number,
        required:true,
        min:0,
    },
    planetID:{
        type:Number,
        required:true,
        min:0,
        max:MAX_PLAN,
    },
    
    questionID:{
        unique: true,
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
const PlanetQn= mongoose.model('PlanetQuestion', planetQnSchema);
module.exports=PlanetQn;