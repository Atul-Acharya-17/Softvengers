const mongoose = require('mongoose');
const Schema = mongoose.Schema;

//defining the schema of world, i.e. assignment and solar system order identifier
const worldSchema = new Schema({
    universeUnlockingOrder:{
        universeName: {
            type: String,
            required: true,
            unique: true
        },
        universeOrder: {
            type: Number,
            required: true,
            unique: true,
            index: true,
            default: 0,
            min: 0
        },
        solarSystemUnlockingOrder: {
            solarSystemName: {
                type: String,
                required: true,
                unique: true
            },
            solarSystemOrder: {
                type: Number,
                required: true,
                unique: true,
                index: true,
                default: 0, 
                min: 0
            }
        }
    }
});

//making the mongoose model and exporting it
const World = mongoose.model('World', worldSchema);
module.exports = World;