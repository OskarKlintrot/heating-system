import * as Mongoose from 'mongoose'

export interface IRoom {
  _id: string
  roomDescription: string
  heatingOn: boolean
  lastTemperature: number
  automaticControl: boolean
}

export interface IRoomModel extends IRoom, Mongoose.Document {
  _id: string
}

const roomSchema: Mongoose.Schema = new Mongoose.Schema({
  roomDescription: { type: String, required: true },
  heatingOn:  { type: Number, required: true },
  lastTemperature: { type: Number, required: true },
  automaticControl: { type: Boolean, required: true },
})

export const Room: Mongoose.Model<IRoomModel> = Mongoose.model<IRoomModel>('Room', roomSchema)
