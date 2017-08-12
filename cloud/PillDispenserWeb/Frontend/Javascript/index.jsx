import React from 'react';
import ReactDOM from 'react-dom';
import { AnotherComponent } from './AnotherComponent.jsx';

class TestComponent extends React.Component {
    render() {
        return (<div> hello world with class <AnotherComponent /> </div>);
    }
};

// Render the component in the dom
ReactDOM.render(
    <TestComponent />,
    document.getElementById("container")
);