import { Temperature } from '../../../src/models/temperature'
import * as Mongoose from 'mongoose'

const expect = chai.expect

describe('Test Temperature model', () => {

  it('should exist errors if nothing is assigned to the properties', (done: MochaDone) => {
      const temperature = new Temperature()

      // Make sure 'temperature' got a timestamp from Date.now()
      expect(temperature.timestamp).to.be.a.instanceOf(Date)
      expect(temperature.timestamp).to.be.lessThan(Date.now())
      expect(temperature.timestamp).to.be.greaterThan(Date.now() - 50)

      temperature.validate((err: any) => {
        expect(err.errors.roomId).to.exist
        expect(err.errors.temp).to.exist
        done()
      })
    })

    it('should not exist errors if all properties are assigned', (done: MochaDone) => {
      const temperature = new Temperature()

      temperature.timestamp = new Date()
      temperature.roomId = Mongoose.mongo.ObjectID.createFromTime(Date.now()).toString()
      temperature.temp = 20.7

      temperature.validate((err: any) => {
        expect(err).to.not.exist
        done()
      })
    })

    it('should not be possible to assign a regular string to roomId', (done: MochaDone) => {
      const temperature = new Temperature()

      temperature.roomId = 'I am just a regular string'

      temperature.validate((err: any) => {
        expect(err.errors.roomId).to.exist
        done()
      })
    })

})
