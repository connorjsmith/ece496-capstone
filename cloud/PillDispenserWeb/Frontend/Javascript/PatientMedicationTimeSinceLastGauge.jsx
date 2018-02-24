import React from 'react';
import ReactHighcharts from 'react-highcharts';
import { Card, Spin } from 'antd';

const defaultCardWidth = 1000;
const defaultCardHeight = 1000;
const commonCardConfig = {
    noHovering: false,
    bordered: true,
    height: 1,
    width: 1
}

const gaugeConfig = {
    chart: {
        height: 300
    },
    credits: { enabled: false },
    title: {
        text: '30-day Medication Compliance'
    },
    subtitle: {
        text: 'Sample data used'
    },
    plotOptions: {
        pie: {
            dataLabels: {
                enabled: true,
                distance: 0,
                style: {
                    fontWeight: 'bold',
                    color: 'white'
                }
            },
            startAngle: -90,
            endAngle: 90,
            center: ['50%', '75%']
        }
    }
}

let chartReflow = undefined;

export class PatientMedicationTimeSinceLastGauge extends React.Component {
    constructor(props) {
        super(props);
        this.endpointURL = props.endpointURL;
        this.chartConfig = JSON.parse(JSON.stringify(gaugeConfig));
        this.cardConfig = JSON.parse(JSON.stringify(commonCardConfig));
        this.state = {
            chartLoading: true
        };
        Object.assign(this.chartConfig, props.chartConfig);
        Object.assign(this.cardConfig, props.cardConfig);
        this.chartConfig.chart.height = defaultCardHeight / this.cardConfig.height - 50; // subtract 50 for padding
    }
    onChartLoaded() {
        this.setState({ chartLoading: false });
    }
    render() {
        let highcharts = <ReactHighcharts isPureConfig={true} neverReflow={true} config={this.chartConfig} callback={this.onChartLoaded.bind(this)}> </ReactHighcharts >;
        return (
            <Card
                bordered={this.cardConfig.bordered}
                noHovering={this.cardConfig.noHovering}
                style={{
                    width: defaultCardWidth / this.cardConfig.width,
                    height: defaultCardHeight / this.cardConfig.height
                }}
            >
                <Spin spinning={this.state.chartLoading}>
                    {highcharts}
                </Spin>
            </Card>
        );
    }
}