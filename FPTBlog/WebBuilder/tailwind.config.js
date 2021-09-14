const theme = require('tailwindcss/defaultTheme');

module.exports = {
    purge: ['./../Views/**/*.cshtml', './../wwwroot/**/*.js'],
    darkMode: false, // or 'media' or 'class'
    theme: {
        extend: {},
        colors: {
            ...theme.colors,
            tango: {
                DEFAULT: '#F37124',
                50: '#FFFEFE',
                100: '#FEEEE5',
                200: '#FBCFB5',
                300: '#F8B085',
                400: '#F69054',
                500: '#F37124',
                600: '#D8580C',
                700: '#A84409',
                800: '#773107',
                900: '#471D04',
            },
            'venice-blue': {
                DEFAULT: '#0751A0',
                50: '#92C5FA',
                100: '#7AB7F9',
                200: '#499DF7',
                300: '#1883F5',
                400: '#096AD1',
                500: '#0751A0',
                600: '#05386F',
                700: '#03203E',
                800: '#01070D',
                900: '#000000',
            },
            apple: {
                DEFAULT: '#52B848',
                50: '#EEF8ED',
                100: '#DDF1DB',
                200: '#BAE3B6',
                300: '#97D591',
                400: '#75C66D',
                500: '#52B848',
                600: '#419439',
                700: '#316F2B',
                800: '#214A1D',
                900: '#11250F',
            },
        },
    },
    variants: {
        extend: {},
    },
    plugins: [],
};
