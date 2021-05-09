const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const MIN_TUT=2;
const MAX_TUT=4;

  //defining the schema of assignments
const assignmentSchema = new Schema({
    assignmentID:{
        unique: true,
        type: Number,
        index: true,
        required: true,
        min:1
    },
    assignmentName:{
        unique: true,
        type: String,
        required: true
    },
    timeLimit:{
        type: Number,
        required:true,
        min:1
    },
    questionIDs:{
        type: [Number],
        required: true,
        min:1
    },
    deadline:{
        type: Date,
        required: true
    },
    tutGrp:{
        type: String,
        required: true,
        min:MIN_TUT,
        max:MAX_TUT
    }
});

//making the mongoose model and exporting it
const Assignment= mongoose.model('Assignment', assignmentSchema);
module.exports=Assignment;