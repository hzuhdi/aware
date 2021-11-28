// assets
import { IconHome } from '@tabler/icons';

// constant
const icons = { IconHome };

// ==============================|| HOMEPAGE MENU ITEMS ||============================== //

const homepage = {
    id: 'home',
    title: 'Home',
    type: 'group',
    children: [
        {
            id: 'home-page',
            title: 'Homepage',
            type: 'item',
            url: '/',
            icon: icons.IconHome,
            breadcrumbs: false
        }
    ]
};

export default homepage;
