/** @type {import('tailwindcss').Config} */
module.exports = {
    darkMode: "class",
    content: ["./**/*.{razor,html,cshtml}"],
    theme: {
        extend: {
            colors: {
                
                'primary': {
                    '50': '#f0f5fe',
                    '100': '#dee8fb',
                    '200': '#c4d9f9',
                    '300': '#9cc0f4',
                    '400': '#72a2ee',
                    '500': '#4a7ce7',
                    '600': '#355fdb',
                    '700': '#2c4bc9',
                    '800': '#2a3fa3',
                    '900': '#273981',
                    '950': '#1c254f',
                },


            },
        },
    },
    plugins: [
        require('@tailwindcss/forms'),
        require('@tailwindcss/aspect-ratio'),
        require('tailwindcss-animated'),

    ],
};
