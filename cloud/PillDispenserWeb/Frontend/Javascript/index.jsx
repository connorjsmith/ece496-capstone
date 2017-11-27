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
    title: {
        text: 'Medication Timeliness between 2014 - 2015'
    }, series: [{
        name: 'Percentage',
        colorByPoint: true,
        data: [{
            name: 'No',
            y: 56.00
        }, {
            name: 'Yes',
            y: 44.00,
            sliced: true,
            selected: true
        }]
    }]
};

ReactDOM.render(
    <PatientMedicationPieChartCard chartConfig={chartConfig} />, // TODO: we will replace this with a dashboard component,
    document.getElementById("container")                         // which will have several of these charts inside it
);