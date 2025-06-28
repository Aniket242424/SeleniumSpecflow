# Use official .NET SDK 8 image
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Install required packages
RUN apt-get update && apt-get install -y \
    wget unzip curl gnupg \
    libgdiplus libxml2 libcurl4-openssl-dev \
    libssl3 libssl-dev ca-certificates \
    jq

# Install Google Chrome Stable
RUN wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb && \
    apt install -y ./google-chrome-stable_current_amd64.deb && \
    rm google-chrome-stable_current_amd64.deb

# Get Chrome version and install matching ChromeDriver
RUN CHROME_VERSION=$(google-chrome --version | grep -oP '\d+\.\d+\.\d+' | head -1) && \
    echo "Chrome version: $CHROME_VERSION" && \
    DRIVER_URL=$(curl -s "https://googlechromelabs.github.io/chrome-for-testing/last-known-good-versions-with-downloads.json" \
      | jq -r --arg ver "$CHROME_VERSION" '.channels.Stable.downloads.chromedriver[] | select(.platform=="linux64") | .url') && \
    wget -O chromedriver.zip "$DRIVER_URL" && \
    unzip chromedriver.zip && \
    mv chromedriver-linux64/chromedriver /usr/local/bin/ && \
    chmod +x /usr/local/bin/chromedriver && \
    rm -rf chromedriver.zip chromedriver-linux64

# Set working directory inside container
WORKDIR /app

# Copy all project files into container
COPY . .

# Restore and build
RUN dotnet restore
RUN dotnet build --no-restore

# Create directory for test results
RUN mkdir -p TestResults

# Run tests and generate HTML report in /app/TestResults
CMD ["dotnet", "test", "--no-build", "--logger:html;LogFileName=/app/TestResults/TestReport.html"]
