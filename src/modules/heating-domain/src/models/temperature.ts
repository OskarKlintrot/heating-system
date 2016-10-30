import * as Mongoose from 'mongoose'

export interface ITemperature {
  _id: string
  timestamp: Date
  roomId: string
  temp: number
}

export interface ITemperatureModel extends ITemperature, Mongoose.Document {
  _id: string
}

const TemperatureSchema: Mongoose.Schema = new Mongoose.Schema({
  timestamp: { type: Date, required: true, default: Date.now },
  roomId: { type: Mongoose.Schema.Types.ObjectId, required: true, ref: 'Room' },
  temp: { type: Number, required: true },
})

export const Temperature: Mongoose.Model<ITemperatureModel> = Mongoose.model<ITemperatureModel>('Temperature', TemperatureSchema)
