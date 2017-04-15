declare namespace SignalR {
    interface Connection {
        server: Server;
        client: Client;
    }

    interface Server {

    }

    interface Client {
        setFibonacciSeriesMessage(message: any);
    }
}

interface SignalR {
    example: SignalR.Connection;
}
