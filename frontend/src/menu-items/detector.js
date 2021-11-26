// assets
import { IconDashboard, IconPhoto, IconBrandYoutube, IconHeadset } from '@tabler/icons';

// constant
const icons = { IconDashboard, IconPhoto, IconBrandYoutube, IconHeadset };

// ==============================|| DASHBOARD MENU ITEMS ||============================== //

const dashboard = {
    id: 'detector',
    title: 'Detector',
    type: 'group',
    children: [
        {
            id: 'detector-video',
            title: 'Video',
            type: 'item',
            url: '/detector/video',
            icon: icons.IconBrandYoutube,
            breadcrumbs: false
        },
        {
            id: 'detector-audio',
            title: 'Audio',
            type: 'item',
            url: '/detector/audio',
            icon: icons.IconHeadset,
            breadcrumbs: false
        },
        {
            id: 'detector-image',
            title: 'Image',
            type: 'item',
            url: '/detector/image',
            icon: icons.IconPhoto,
            breadcrumbs: false
        }
    ]
};

export default dashboard;
