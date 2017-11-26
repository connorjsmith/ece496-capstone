import React from 'react';
import ReactHighcharts from 'react-highcharts';
import { Card, Spin } from 'antd';

const defaultCardWidth = 500;
const commonCardConfig = {
    noHovering: true,
    bordered: true
}
const commonChartConfig = {
    chart: {
        plotBackgroundColor: null,
        plotBorderWidth: null,
        animation: true,
        plotShadow: false,
        type: 'pie'
    },
    credits: { enabled: false },
    width: null,
    height: null,
    tooltip: {
        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
    },
    plotOptions: {
        pie: {
            allowPointSelect: true,
            cursor: 'pointer',
            dataLabels: {
                enabled: true,
                format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                style: {
                    color: (ReactHighcharts.Highcharts.theme && ReactHighcharts.Highcharts.theme.contrastTextColor) || 'black'
                }
            }
        }
    },
}

let chartReflow = undefined;

export class PatientMedicationPieChartCard extends React.Component {
    constructor(props) {
        super(props);
        this.endpointURL = props.endpointURL;
        this.chartConfig = JSON.parse(JSON.stringify(commonChartConfig));
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