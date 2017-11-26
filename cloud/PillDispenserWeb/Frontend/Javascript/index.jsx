import React from 'react';
import ReactDOM from 'react-dom';
import { AnotherComponent } from './AnotherComponent.jsx';
import { PatientMedicationPieChartCard } from './PatientMedicationPieChartCard.jsx';

class TestComponent extends React.Component {
    render() {
        return (<div> hello world with class <AnotherComponent /> </div>);
    }
};

// Render the component in the dom
let chartConfig = {
    series: [{
        name: 'Brands',
        colorByPoint: true,
        data: [{
            name: 'IE',
            y: 56.33
        }, {
            name: 'Chrome',
            y: 24.03,
            sliced: true,
            selected: true
        }, {
            name: 'Firefox',
            y: 10.38
        }, {
            name: 'Safari',
            y: 4.77
        }, {
            name: 'Opera',
            y: 0.91
        }, {
            name: 'Other',
            y: 0.2
        }]
    }]
};

ReactDOM.render(
    <PatientMedicationPieChartCard chartConfig={chartConfig} />, // TODO: we will replace this with a dashboard component,
    document.getElementById("container")                         // which will have several of these charts inside it
);