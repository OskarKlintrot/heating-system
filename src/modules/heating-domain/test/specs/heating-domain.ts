import * as Domain from '../../src'

const expect = chai.expect

describe('Test main entry point', () => {

    it('should work!', () => {
        expect(true).to.eql(true)
    })

    it('should be an empty object', () => {
        expect(typeof Domain).to.eql('object')
    })

    it('should be a function', () => {
        expect(typeof Domain.Room).to.eql('function')
    })

    it('should be a function', () => {
        expect(typeof Domain.Temperature).to.eql('function')
    })
})
