"use strict";
// originally taken from https://github.com/drew-wallace/LittleJohn as an example project
import WinJS from 'winjs';
import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { createStore, applyMiddleware } from 'redux';
import thunkMiddleware from 'redux-thunk';
import createLogger from 'redux-logger'
import _ from 'lodash';
// import rgbHex from 'rgb-hex';

let app = WinJS.Application;

const activation = Windows.ApplicationModel.Activation;

const loggerMiddleware = createLogger();

// let store = createStore(<reducer>, <initial state>,
      // applyMiddleware(
          // thunkMiddleware/*,          
          // loggerMiddleware*/
      // )
// );

app.onactivated = function (args) {
    let targetEl = document.getElementById("main")
    if (args.detail.kind === activation.ActivationKind.launch) {
        if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
            // TODO: This application has been newly launched. Initialize your application here.
        } else {
            // TODO: This application was suspended and then terminated.
            // To create a smooth user experience, restore application state here so that it looks like the app never stopped running.
        }

        console.log(process.env.NODE_ENV);

        ReactDOM.render(
            <div>
                <h1> Hello World! </h1>
            </div>,
            targetEl
            // document.getElementById("main")
        );
    }
};

app.oncheckpoint = function (args) {
    // TODO: This application is about to be suspended. Save any state that needs to persist across suspensions here.
    // You might use the WinJS.Application.sessionState object, which is automatically saved and restored across suspension.
    // If you need to complete an asynchronous operation before your application is suspended, call args.setPromise().

    // const state = store.getState();
    // app.sessionState = {...state};
};

app.onbackclick = function (event) {
      store.dispatch(undoTitle());
      // Need to return true to cancel the default behavior of this event.
      return true;
}

app.start();
