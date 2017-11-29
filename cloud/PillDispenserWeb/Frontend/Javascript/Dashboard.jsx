import React from 'react';
import ReactDOM from 'react-dom';
import { AnotherComponent } from './AnotherComponent.jsx';
import { CardContainer } from './CardContainer.jsx';
import { PatientMedicationPieChartCard } from './PatientMedicationPieChartCard.jsx';

// Render the component in the dom
let pieConfig = {
    title: {
        text: 'Medication Timeliness between<br>2014 - 2015'
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
    <CardContainer>
        <PatientMedicationPieChartCard chartConfig={pieConfig} />
        <PatientMedicationPieChartCard chartConfig={pieConfig} />
        <PatientMedicationPieChartCard chartConfig={pieConfig} />
        <PatientMedicationPieChartCard chartConfig={pieConfig} />
    </CardContainer>,
    document.getElementById("container")                         // which will have several of these charts inside it
);