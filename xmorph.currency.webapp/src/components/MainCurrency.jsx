import React, { Fragment } from 'react'
import Form from 'react-bootstrap/Form'

export class MainCurrency extends React.Component {
  render () {
    return (
            <Fragment>
                <div className='container'>
                    <div className='row full-width-center'>
                        <div className='col-lg-2'></div>
                        <div className='col-lg-8 col-xs-12 row'>
                            <div className='col-lg-6 col-xs-12 control-padding'>
                                <Form.Control placeholder='Dolares' className='Input-Full-Width'></Form.Control>
                            </div>
                            <div className='col-lg-6 col-xs-12 control-padding'>
                                <Form.Control placeholder='Soles'></Form.Control>
                            </div>
                        </div>
                        <div className='col-lg-2'></div>
                    </div>
                </div>
            </Fragment>
    )
  }
}
