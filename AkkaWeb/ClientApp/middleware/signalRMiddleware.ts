import * as Fibonacci from '../store/Fibonacci';

function registerGlobalHandlers() {
    const getConnectionStateChange = (change: SignalR.StateChanged) => {
        const getName = (state: keyof SignalR.StateChanged) => Object.keys($.signalR.connectionState)[change[state]] || 'unknown';

        return {
            oldState: getName('oldState'),
            newState: getName('newState')
        };
    };

    $.connection.hub.error(err => {
        console.warn(err);
    });

    $.connection.hub.stateChanged(change => {
        const stateChange = getConnectionStateChange(change);
        console.info(`${stateChange.oldState} => ${stateChange.newState}`);
    });
}

const exampleHub = $.connection.example;

const signalrMiddleware = ({ dispatch }) =>  {
    registerGlobalHandlers();

    $.connection.hub.start({ transport: 'auto', jsonp: false });

    exampleHub.client.setFibonacciSeriesMessage = message => {
        console.info(message);
        dispatch(Fibonacci.actionCreators.addSeries({ series: message, source: message.length }))
    };

    type KnownAction = Fibonacci.AddSeriesAction;

    return next => (action: KnownAction) => {
        switch (action.type) {
            // case 'SOME_ACTION':
            //     exampleHub.server.doSomething(action.state);
            //     return next(action);
            default:
                return next(action);
        }
    };
};

export default signalrMiddleware;
