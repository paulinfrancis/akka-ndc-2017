import * as React from 'react';
import { Link } from 'react-router';
import { connect } from 'react-redux';
import { ApplicationState }  from '../store';
import * as FibonacciStore from '../store/Fibonacci';
import './style.scss';

type IFibonacciProps = FibonacciStore.IFibonacciState & typeof FibonacciStore.actionCreators;

export const Fibonacci = (props: IFibonacciProps) => {
    return (
        <div className='fibonacci'>
            <ul>
                {
                    props.fibonacciSeries.map((f, i) => {
                        return <li key={ i }>{ f.series.length } => { JSON.stringify(f.series) }</li>
                    })
                }
            </ul>
        </div>
    );
}

export default connect(
    (state: ApplicationState) => state.fibonacciSeries,
    FibonacciStore.actionCreators
)(Fibonacci);
