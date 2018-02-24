import React from 'react';
import ReactDOM from 'react-dom';
import { AnotherComponent } from './AnotherComponent.jsx';
import { CardContainer } from './CardContainer.jsx';
import { PatientMedicationPieChartCard } from './PatientMedicationPieChartCard.jsx';
import { PatientMedicationScatterPlot } from './PatientMedicationScatterPlot.jsx';
import { PatientMedicationTimeSinceLastGauge } from './PatientMedicationTimeSinceLastGauge.jsx';

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
            sliced: false,
            selected: false
        }]
    }]
};

let scatterConfig = {
    series: [{
        name: 'Epinephrine',
        color: 'rgba(223, 83, 83, .5)',
        data: [
            { name: 'Monday', y: Date.UTC(2017, 1, 1, 9, 30) },
            { name: 'Monday', y: Date.UTC(2017, 1, 1, 5) },
            { name: 'Tuesday', y: Date.UTC(2017, 1, 1, 18, 45) },
            { name: 'Wednesday', y: Date.UTC(2017, 1, 1, 18, 25) },
            { name: 'Thursday', y: Date.UTC(2017, 1, 1, 18, 45) },
            { name: 'Friday', y: Date.UTC(2017, 1, 1, 19, 45) },
            { name: 'Saturday', y: Date.UTC(2017, 1, 1, 18, 47) },
            { name: 'Friday', y: Date.UTC(2017, 1, 1, 12, 45) },
            { name: 'Monday', y: Date.UTC(2017, 1, 1, 16, 45) },
            { name: 'Sunday', y: Date.UTC(2017, 1, 1, 18, 45) },
            { name: 'Monday', y: Date.UTC(2017, 1, 1, 10, 30) }
        ]
    }]
};

let gaugeConfig = {
    series: [{
        type: 'pie',
        name: null,
        innerSize: '50%',
        data: [
            {
                name: 'Taken',
                y: 22
            },
            {
                name: 'Missed',
                y: 3
            },
            {
                name: 'Upcoming',
                y: 5,
            }
        ]
    }]
}


ReactDOM.render(
    <CardContainer>
        <PatientMedicationPieChartCard chartConfig={pieConfig} cardConfig={{ width: 2, height: 2 }} />
        <PatientMedicationScatterPlot chartConfig={scatterConfig} cardConfig={{ width: 2, height: 2 }}/>
        <PatientMedicationTimeSinceLastGauge chartConfig={gaugeConfig} cardConfig={{ width: 3, height: 3 }}/>
        <PatientMedicationTimeSinceLastGauge chartConfig={gaugeConfig} cardConfig={{ width: 3, height: 3 }}/>
        <PatientMedicationTimeSinceLastGauge chartConfig={gaugeConfig} cardConfig={{ width: 3, height: 3 }}/>
    </CardContainer>,
    document.getElementById("container")                         // which will have several of these charts inside it
);