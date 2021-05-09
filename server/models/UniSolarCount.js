const mongoose = require('mongoose');
const Schema = mongoose.Schema;


const MIN_UNI=0;
const MAX_UNI=5;

const uniSolarCount=new Schema({
    universeID:{
        type:Number,
        min:MIN_UNI,
        max:MAX_UNI,
        required:true,
        unique:true,
    },
    noOfSolar:{
        type:Number,
        min:1,
        required:true,
        default:1,
    }
});



const UniSolarCount= mongoose.model('UniSolarCount', uniSolarCount);
module.exports=UniSolarCount;