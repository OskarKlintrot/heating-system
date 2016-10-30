import { Room } from '../../../src/models/room'

const expect = chai.expect

describe('Test Room model', () => {

  it('should exist errors if nothing is assigned to the properties', (done: MochaDone) => {
      const room = new Room()

      room.validate((err: any) => {
        expect(err.errors.roomDescription).to.exist
        expect(err.errors.heatingOn).to.exist
        expect(err.errors.lastTemperature).to.exist
        expect(err.errors.automaticControl).to.exist
        done()
      })
    })

    it('should not exist errors if all properties are assigned', (done: MochaDone) => {
      const room = new Room()

      room.roomDescription = 'Description of the room'
      room.heatingOn = true
      room.lastTemperature = 20.7
      room.automaticControl = false

      room.validate((err: any) => {
        expect(err).to.not.exist
        done()
      })
    })

})
