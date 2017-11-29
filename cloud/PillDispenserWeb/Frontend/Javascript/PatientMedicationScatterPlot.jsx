import React from 'react';
import ReactHighcharts from 'react-highcharts';
import { Card, Spin } from 'antd';

const defaultCardWidth = 500;
const commonCardConfig = {
    noHovering: false,
    bordered: true
}

const scatterPlotConfig = {
    chart: {
        type: 'scatter',
        zommType: 'xy'
    },
    credits: { enabled: false },
    title: {
        text: 'Patient medication times'
    },
    subtitle: {
        text: 'Sample data used'
    },
    xAxis: {
        title: {
            enabled: true,
            text: 'Date'
        },
        type: 'category',
        categories: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
        startOnTick: true,
        endOnTick: true,
        showLastLabel: true
    },
    yAxis: {
        title: {
            text: 'Time'
        },
        type: 'datetime'
    },
    legend: {
        layout: 'horizontal',
        align: 'center',
        verticalAlign: 'bottom',
        y: 10,
        floating: true,
        enabled: true,
        backgroundColor: '#FFFFFF',
        borderWidth: 1
    },
    plotOptions: {
        scatter: {
            marker: {
                radius: 5,
                states: {
                    hover: {
                        enabled: true,
                        lineColor: 'rgb(100,100,100)'
                    }
                }
            },
            states: {
                hover: {
                    marker: {
                        enabled: false
                    }
                }
            }
        }
    }
}

let chartReflow = undefined;

export class PatientMedicationScatterPlot extends React.Component {
    constructor(props) {
        super(props);
        this.endpointURL = props.endpointURL;
        this.chartConfig = JSON.parse(JSON.stringify(scatterPlotConfig));
        this.cardConfig = JSON.parse(JSON.stringify(commonCardConfig));
        this.state = {
            chartLoading: true
        };
        Object.assign(this.chartConfig, props.chartConfig);
        Object.assign(this.cardConfig, props.chartConfig);
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
                style={{ width: defaultCardWidth }}
            >
                <Spin spinning={this.state.chartLoading}>
                    {highcharts}
                </Spin>
            </Card>
        );
    }
}