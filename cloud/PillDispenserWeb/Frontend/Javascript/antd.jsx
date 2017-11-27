import React from 'react';
import ReactDOM from 'react-dom';
// TODO: should change this to proper antd/lib/** imports to reduce dist.js size
import { Calendar, LocaleProvider, Card, Timeline } from 'antd';
import enUS from 'antd/lib/locale-provider/en_US';

ReactDOM.render(
    <div>
        <LocaleProvider locale={enUS}>
            <Calendar />
        </LocaleProvider>
        <Card title="Test Card Title" extra={<a href="#">More info</a>} style={{ width: 300 }}>
            <p> Card line 1</p>
            <p> Card line 2</p>
            <p> Card line 3</p>
        </Card>
        <Timeline style={{ marginTop: "100px" }}>
            <Timeline.Item color="green">line 1</Timeline.Item>
            <Timeline.Item color="green">line 2</Timeline.Item>
            <Timeline.Item color="red">
                <p>problem 1</p>
                <p>problem 2</p>
                <p>problem 3</p>
            </Timeline.Item>
            <Timeline.Item>
                <p>Technical testing 1</p>
                <p>Technical testing 2</p>
                <p>Technical testing 3 2015-09-01</p>
            </Timeline.Item>
        </Timeline>
    </div>,
    document.getElementById("root")
);
