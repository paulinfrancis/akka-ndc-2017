import { Action, Reducer, combineReducers } from 'redux';

export interface IFibonacciSeries {
    source: number;
    series: number[];
}

export interface IFibonacciState {
    fibonacciSeries: IFibonacciSeries[]
}

export interface AddSeriesAction { type: 'ADD_SERIES_ACTION'; series: IFibonacciSeries; }

type KnownAction = AddSeriesAction;

export const actionCreators = {
    addSeries: (series: IFibonacciSeries) => <AddSeriesAction> { type: 'ADD_SERIES_ACTION', series },
};

const fibonacciReducer: Reducer<IFibonacciSeries[]> = (state: IFibonacciSeries[] = [], action: KnownAction) => {
    switch (action.type) {
        case 'ADD_SERIES_ACTION': {
            return [ action.series, ...state ];
        } 
    }
    return state;
};

export const reducer = <Reducer<IFibonacciState>>combineReducers({
    fibonacciSeries: fibonacciReducer,
});
