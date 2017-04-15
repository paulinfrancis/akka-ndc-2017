import * as React from 'react';
import { Router, Route, HistoryBase } from 'react-router';
import { Layout } from './components/Layout';
import Counter from './components/Fibonacci';

export default <Route component={ Layout }>
    <Route path='/' components={{ body: Counter }} />
</Route>;

// Enable Hot Module Replacement (HMR)
if (module.hot) {
    module.hot.accept();
}
